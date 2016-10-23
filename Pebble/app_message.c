//ONESSIE IS NOT CAPITOL ONE. IT IS MADE BY CAPITAL ONE, BUT CAPITAL ONE API != ONE NESSie.so dont screw that up next time
//everything done in js is actually done from the phone. the phone recieves data and sends stuff back to the watch
// Listen for when an AppMessage is received
//if the watch successfully recieves the message, the phone(js) knows

//MESSAGE_KEY VALUES: "create dictionary to send in this order"
//balance - 0

//merchant ids
//


//-----Event listeners//

Pebble.addEventListener('appmessage',
  function(e) {
    console.log('AppMessage received!');
  }                     
);

// Listen for when the watchface is opened
Pebble.addEventListener('ready', 
  function(e) {
    console.log('PebbleKit JS ready!');

    // Get the initial weather
    getAccountData();
    getPurchaseData();
  }
);

setInterval(intervalFunction,1000);
function intervalFunction(){
  getAccountData();
  getPurchaseData();
  getOverflowData();
}
//--------------------//

//this creates an http request
//type is GET or POST
//url is the url with the key
//callback is idk?
//xhrRequest = function (url, type, callback)
/**IMPORTANT**/
//ACCOUNT id is 56c66be5a73e492741506f2b. MULTIPLE CUSTOMERS CAN MAKE ACCOUNTS BASED OFF OF THIS ID. THIS IS NOT A CUSTOMER ID!@#$@$@#$#@$@#$@#$@#$@$#@$@#$

var xhrRequest = function (url, type, callback) {
  var xhr = new XMLHttpRequest();
  xhr.onload = function () {
    callback(this.responseText);
  };
  xhr.open(type, url);
  xhr.send();
};
//in oreder to create a checking or credit account
//1. create an customer with post on nessie
//2. create said account after using customer id
//3. your done
//responseText is the result of the query in string(JSON) format
//convert to json js obj with js
//depending on the field from the api, use em to get certain information

//globals
var keys = require('message_keys');

// Build a dictionary. if keys invalid is not 0, watch says budget contraints and vibrate also
var dict = {};

function getAccountData(){
  
  //var url = 'http://api.reimaginebanking.com/accounts/580b8d3b360f81f104544ced?key=8718dbc034540cb9f0b1b76d42452753';//this is the main checking account
  var url = 'http://api.reimaginebanking.com/accounts/580c2363360f81f104544f26?key=8718dbc034540cb9f0b1b76d42452753';//this is the budget checking account
  
  xhrRequest(url, 'GET',  function(responseText) {
    
    console.log(responseText);
    var json = JSON.parse(responseText);
    
    //dictionary HAS TO MATCH NAMES IN SETTINGS. INCLUDING CAPS AND ORDER
    //dict[keys.BALANCE] = ((json.balance+''));
     
    sendData(keys.BALANCE,'$'+json.balance+'');
     
    
    });
   
}

//this gets the most recent purchase data and adds its description to the stuff
function getPurchaseData(){
  
  //var url = 'http://api.reimaginebanking.com/accounts/580b8d3b360f81f104544ced/purchases?key=8718dbc034540cb9f0b1b76d42452753';//this checks all purchases from the MAIN ACCOUNT checking account
  var url = 'http://api.reimaginebanking.com/accounts/580c2363360f81f104544f26/purchases?key=8718dbc034540cb9f0b1b76d42452753';//this checks all purchases from the BUDGET ACCOUNT checking account
  
  
  xhrRequest(url, 'GET',  function(responseText) {
    
    var json = JSON.parse(responseText);
    var purchaseLength = json.length;
    
    //console.log('Data length:--------------- '+purchaseLength);
    if(purchaseLength > 0){
      var fullMerchantData = 'Spent $'+json[purchaseLength-1].amount+'\nOn '+json[purchaseLength-1].description;//info on what was spent
      //sendData(keys.BALANCE,json.balance+'');
      dict[keys.FULLMERCHANT] = fullMerchantData;
      sendData(keys.FULLMERCHANT, fullMerchantData);
    }
    //var status =  json[purchaseLength-1].status;
   
    
  });
    
   
}

function getOverflowData(){
  
  var overflowURL = 'http://api.reimaginebanking.com/bills/580c3da7360f81f104544f5c?key=8718dbc034540cb9f0b1b76d42452753';
  
  xhrRequest(overflowURL, 'GET',  function(responseText) {
   var json = JSON.parse(responseText);
    console.log('flow?: '+json.nickname);
    if(json.nickname == 'OVER'){
      dict[keys.OVERFLOW] = 1;
      sendData(keys.OVERFLOW, dict[keys.OVERFLOW]);
      
    }
    else{
       dict[keys.OVERFLOW] = 0;
      sendData(keys.OVERFLOW, dict[keys.OVERFLOW]);
    }
    
});
    
   
}
//sends the data to the pebble watch face
function sendData(key, data){
  dict[key] = data;
  Pebble.sendAppMessage(dict, function() {
   }, function(e) {
   console.log('Message failed: ' + JSON.stringify(e));
  });
}

