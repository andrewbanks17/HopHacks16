//require('dotenv').load();

var http       = require('http')
  , AlexaSkill = require('./AlexaSkill')
  , CAP_KEY='8718dbc034540cb9f0b1b76d42452753'
  , MER_KEY='580c2363360f81f104544f26'
  , APP_ID='amzn1.ask.skill.201c2cb1-91f8-45a1-8e4c-c31331588f21';

var url = function() {
  return 'http://api.reimaginebanking.com/accounts/' + MER_KEY + '/bills?key=' + CAP_KEY;
};

var getJsonFromCap = function(callback, errcallback){
  http.get(url(), function(res){
    var body = '';

    res.on('data', function(data){
      body += data;
    });

    res.on('end', function(){
      var result = JSON.parse(body);
      callback(result);
    });

  }).on('error', function(e){
    errcallback(e);
  });
};

var handleOverflow = function(intent, session, response){
    getJsonFromCap(function(data){
        if (data[0].nickname){
            var cardText = data[0].nickname;
            if (cardText == "UNDER") {
                var text = 'You have enough money!';
            } else if (cardText == "OVER") {
                var text = 'Overflow! You cannot spend anymore for the day!';
            } else {
                var text = 'Cannot access information!';
            }           
        } else {
            var text = 'Information does not exist.';
            var cardText = text;
        }

        var heading = text;
        response.tellWithCard(text, heading, cardText);
    },
    function(e){
        response.tellWithCard('Err', 'Err', 'Err');
    });
};

var BillInCheckingAccount = function(){
  AlexaSkill.call(this, APP_ID);
};

BillInCheckingAccount.prototype = Object.create(AlexaSkill.prototype);
BillInCheckingAccount.prototype.constructor = BillInCheckingAccount;

BillInCheckingAccount.prototype.eventHandlers.onSessionStarted = function(sessionStartedRequest, session){
  // What happens when the session starts? Optional
  console.log("onSessionStarted requestId: " + sessionStartedRequest.requestId
      + ", sessionId: " + session.sessionId);
};

BillInCheckingAccount.prototype.eventHandlers.onLaunch = function(launchRequest, session, response){
  // This is when they launch the skill but don't specify what they want. Prompt
  // them for their bus stop
  var output = 'Welcome to our HopHacks Fall 2016 hack project.';

  var reprompt = 'Do you want to check if you have enough money to make a purchase?';

  response.ask(output, reprompt);

  console.log("onLaunch requestId: " + launchRequest.requestId
      + ", sessionId: " + session.sessionId);
};

BillInCheckingAccount.prototype.intentHandlers = {
  CheckOverflowIntent: function(intent, session, response){
    handleOverflow(intent, session, response);
  }
};

exports.handler = function(event, context) {
    var skill = new BillInCheckingAccount();
    skill.execute(event, context);
};