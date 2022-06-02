/**
 * for communication between RN/WX MiniProgram and Unity WebGL
 * @version 1.0.1
 */

function ReceiveFromGame(cmd, json) {
  console.log('[Bridge] ReceiveFromGame');
  console.log(`[Bridge] cmd: ${typeof cmd} ${cmd}, json : ${typeof json} ${json}`);

  // receive save from unity and notify miniprogram to call api to save
  if (cmd === 'Save') {
    try {
      /* -----------------------------------------------------------------------------------------------------------------------------------------------------------------*/
      /* ATTENTION: Remark: for unity to send player save to miniProgram should be using: miniProgramHybrid.call('webviewSendPlayerSave', { save: <JSON-object-here> });  */
      /* -----------------------------------------------------------------------------------------------------------------------------------------------------------------*/
      miniProgramHybrid.call('webviewSendPlayerSave', { save: JSON.parse(json) });
    } catch (error) {
      console.error(error);
    }
    return;
  }

  SendToReactNative(json);

  // let testJson = "{\"accessToken\":\"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2MTNlZjVlMDk5ZjVjMDNjNzI1NjQ2N2IiLCJpYXQiOjE2MzI5MDE4MDAsImV4cCI6MTYzMzAwMjEwMCwidHlwZSI6ImFjY2VzcyJ9.0KeCnhetYB8XTdB7zvXHqcT9JvMuKWuYMs8blFQkyTc\",\"authUrl\":\"https://service.official.dev.milkcargo.cn/auth/v1/users/me\"}";
  // SendToGame(testJson);
}

let unityInstanceInterval;
function SendToGame(type, content) {
  console.log('[Bridge] SendToGame type: ', type);

  if (window.unityInstance) {
    if (type === 'auth' || type === 'save') {
      const funcName = type === 'auth' ? 'ReceiveAuthFromFrontEnd' : 'ReceiveSaveFromFrontEnd'
      console.log('[Bridge] SendToGame funcName: ', funcName);

      window.unityInstance.SendMessage(
        'InGameBridge',
        funcName,
        content,
      );
    }
  } else {
    console.log('[Bridge] SendToGame unityInstance not found, intervaling...');

    // for preventing unityInstance will be undefined in wechat miniprogram
    unityInstanceInterval = setInterval(() => {
      console.log('[Bridge] intervaling...');
      if (type === 'auth' || type === 'save') {

        clearInterval(unityInstanceInterval);

        const funcName = type === 'auth' ? 'ReceiveAuthFromFrontEnd' : 'ReceiveSaveFromFrontEnd'
        console.log('[Bridge] SendToGame funcName: ', funcName);

        window.unityInstance.SendMessage(
          'InGameBridge',
          funcName,
          content,
        );
      }
    }, 2000);
  }
}

/* ------------------------------ For mini program use ------------------------------ */
let errorSentToMiniProgram = false;

// things that will pass through location.hash from miniProgram
let isMessageComplete = false, dataFromMiniProgram = {};
window.setIsMessageComplete = bool => {
  isMessageComplete = bool;
};

window.setDataFromMiniProgram = obj => {
  dataFromMiniProgram = obj
};

let interval = false;
let sentToUnity = {
  auth: false,
  save: false
};
/* ------------------------------  ------------------------------ */

function SendToReactNative(data) {
  console.log('[Bridge] SendToReactNative');

  if (window.ReactNativeWebView) window.ReactNativeWebView.postMessage(data);

  /* ------------------------------ wx miniprogram ------------------------------ */
  if (window.__wxjs_environment === 'miniprogram') {
    const dataObj = JSON.parse(data);
    console.log('[Bridge] SendToReactNative dataObj:', dataObj);

    // unity first time loaded end, so not yet auth before
    if (dataObj.onLoadEnd && !dataObj.onAuthSuccess) {

      if (!isMessageComplete) {
        console.warn('message is not ready');

        // waiting for miniprogram to pass in necessary info
        interval = setInterval(() => {
          console.log('-=-=-=-=-=-= waiting messageQueue is complete...', isMessageComplete, sentToUnity, dataFromMiniProgram);
          if (isMessageComplete && !sentToUnity.auth && dataFromMiniProgram.dailyRequest) {
            clearInterval(interval);
            sentToUnity.auth = true;

            // dailyRequest is already stringified before
            SendToGame('auth', dataFromMiniProgram.dailyRequest);

            return;
          }
        }, 2000);
      } else {
        sentToUnity.auth = true;

        // dailyRequest is already stringified before
        SendToGame('auth', dataFromMiniProgram.dailyRequest);
        return;
      }
    }

    // auth failed
    if (!dataObj.onLoadEnd && !dataObj.onAuthSuccess && isMessageComplete && sentToUnity.auth) {
      console.error('auth error');
      if (!errorSentToMiniProgram) {
        errorSentToMiniProgram = true;
        miniProgramHybrid.call('webviewAuthFail', {});
        return;
      }
    }

    // auth success and not yet send player save to unity
    if (!dataObj.onLoadEnd && dataObj.onAuthSuccess && isMessageComplete && sentToUnity.auth && !sentToUnity.save && dataFromMiniProgram.save) {
      sentToUnity.save = true;

      // save is already stringified before
      SendToGame('save', dataFromMiniProgram.save);

      return;
    }
  }
  /* ------------------------------ end for mini program use ------------------------------ */
}
function ReceiveDataFromReactNative(json) {
  console.log('[Bridge] ReceiveDataFromReactNative');
  SendToGame('auth', json);
}
window.ReceiveDataFromReactNative = ReceiveDataFromReactNative;
