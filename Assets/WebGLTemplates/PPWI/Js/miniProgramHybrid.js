/**
 * 对象序列化
 * @param {Object} data 要序列化的对象
 * @returns {String}
 */
var serialize = function (data) {
    var s = "";
    for (var p in data)
        s += "&" + p + "=" + encodeURIComponent(data[p]).replace(/\+/gm, "%2B");
    s = s.length > 0 ? s.substring(1) : s;

    return s;
};

/**
 * 生成随机字符串
 * @param {String} [prefix=""] 前缀
 * @param {Number} [len=10] 除前缀外，要随机生成的字符串的长度
 * @returns {String}
 */
var randomString = (function () {
    var i = 0, tailLength = 2;
    var alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";

    var getTail = function () {
        var s = (i++).toString(36);
        if (i > Math.pow(16, tailLength))
            i = 0;

        return s.substring(s.length - tailLength);
    };

    return function (prefix, len) {
        if (arguments.length < 2)
            len = 10;
        if (arguments.length < 1)
            prefix = "";

        var minLen = tailLength + 1;
        if (len < minLen)
            throw new Error("Length should not be little than " + minLen);
        len -= tailLength;

        var str = "";
        while (len-- > 0) {
            var index = Math.floor(Math.random() * alphabet.length);
            str += alphabet.charAt(index);
        }

        return prefix + str + getTail();
    };
})();


var currentReq = null,
    currentCallback = null;

/**
 * 调用微信小程序
 * @param {String} api 业务指令
 * @param {Object} params 业务参数
 * @param {Function} [callback] 处理回调
 */
var callMiniProgram = function (api, params, callback) {
    currentReq = randomString("H5Req_");
    currentCallback = typeof callback === "function" ? callback : null;

    wx.miniProgram.navigateTo({
        url: "/pages/hybrid-page/hybrid?" + serialize({
            cmd: JSON.stringify({
                req: currentReq,
                api: api,
                params: params
            })
        })
    });
};

// decode base64 string
const atob = (string) => {
    // base64 character set, plus padding character (=)
    var b64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
        // Regular expression to check formal correctness of base64 encoded strings
        b64re = /^(?:[A-Za-z\d+\/]{4})*?(?:[A-Za-z\d+\/]{2}(?:==)?|[A-Za-z\d+\/]{3}=?)?$/;

    // atob can work with strings with whitespaces, even inside the encoded part,
    // but only \t, \n, \f, \r and ' ', which can be stripped.
    string = String(string).replace(/[\t\n\f\r ]+/g, "");
    if (!b64re.test(string))
        throw new TypeError("Failed to execute 'atob' on 'Window': The string to be decoded is not correctly encoded.");

    // Adding the padding if missing, for semplicity
    string += "==".slice(2 - (string.length & 3));
    var bitmap, result = "", r1, r2, i = 0;
    for (; i < string.length;) {
        bitmap = b64.indexOf(string.charAt(i++)) << 18 | b64.indexOf(string.charAt(i++)) << 12
            | (r1 = b64.indexOf(string.charAt(i++))) << 6 | (r2 = b64.indexOf(string.charAt(i++)));

        result += r1 === 64 ? String.fromCharCode(bitmap >> 16 & 255)
            : r2 === 64 ? String.fromCharCode(bitmap >> 16 & 255, bitmap >> 8 & 255)
                : String.fromCharCode(bitmap >> 16 & 255, bitmap >> 8 & 255, bitmap & 255);
    }
    return result;
};

let _dailyRequest, _messageQueueSize, _messageQueue = [];
window.addEventListener("hashchange", function (e) {
    try {
        let params = {};

        let messageBase64 = window.location.hash.substr(1);

        messageBase64.split('&').forEach(pair => {
            const key = pair.substring(0, pair.indexOf('='));
            const value = pair.substring(pair.indexOf('=') + 1);

            if (!_dailyRequest && key === 'dailyRequest') {
                _dailyRequest = atob(value);
                console.warn('_dailyRequest set.');
            }

            if (!_messageQueueSize && key === 'messageQueueSize') {
                _messageQueueSize = parseInt(atob(value));
                _messageQueue = [];
                console.warn('_messageQueueSize set.', _messageQueueSize);
            }

            if (_messageQueueSize && _messageQueueSize > 0 && _messageQueue.length < _messageQueueSize && key === 'message') {
                // push each decoded string into the array
                if (!_messageQueue.includes(atob(value))) _messageQueue.push(atob(value));
                console.log('[pushing] _messageQueue', _messageQueue, _messageQueue.length);

                // when the _messageQueue are matched with the _messageQueueSize, then concat all string together
                if (_messageQueue.length === _messageQueueSize) {

                    console.log('[full] _messageQueue', _messageQueue);

                    let saveStr = '';
                    _messageQueue.forEach(decodedStr => saveStr += decodedStr);

                    console.warn('saveStr', typeof saveStr, saveStr);

                    const tempObj = {
                        dailyRequest: _dailyRequest,
                        save: saveStr
                    }

                    console.log('tempObj', tempObj);

                    window.setIsMessageComplete(true);
                    window.setDataFromMiniProgram(tempObj);
                }
            }
        });

        var result = params[currentReq];
        if (typeof currentCallback !== "function") {
            console.log(currentReq + " -> " + result);
        } else
            currentCallback(result);

        // clear history stack for back to previous page in miniprogram by one click
        window.history.back();
    } catch (error) {
        // handle parsing error
        console.error(error);
        callMiniProgram('webviewAuthFail', {});
    }
});

window.miniProgramHybrid = {
    call: callMiniProgram
};