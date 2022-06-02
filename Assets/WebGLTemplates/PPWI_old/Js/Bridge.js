function ReceiveFromGame(cmd, json) {
  console.log("[Bridge] ReceiveFromGame");
  console.log("[Bridge] json :" + json);
  SendToReactNative(json);

  // let testJson = "{\"accessToken\":\"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2MTNlZjVlMDk5ZjVjMDNjNzI1NjQ2N2IiLCJpYXQiOjE2MzM0OTgyMzksImV4cCI6MTYzMzY5ODUzOSwidHlwZSI6ImFjY2VzcyJ9.g8GiADO80I6o-fpXBEu7O-sfh2rUi7fv_QlzbIMa2-k\",\"authUrl\":\"https://service.official.dev.milkcargo.cn/auth/v1/users/me\"}";
  // SendToGame(testJson);
}

function SendToGame(content) {
  console.log("[Bridge] SendToGame");
  if (window.unityInstance)
    window.unityInstance.SendMessage(
      'InGameBridge',
      'ReceiveFromFrontEnd',
      content
    );
}

function SendToReactNative(data) {
  console.log("[Bridge] SendToReactNative");
  if (window.ReactNativeWebView)
    window.ReactNativeWebView.postMessage(data);
}

function ReceiveDataFromReactNative(json) {
  console.log("[Bridge] ReceiveDataFromReactNative");
  SendToGame(json);
}

window.ReceiveDataFromReactNative = ReceiveDataFromReactNative;
