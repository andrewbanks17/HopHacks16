# HopHacks16 / Budget Watch
This project incorporates a budgeting system into the pebble watch and Amazon Alexa. For the case of this project, A user had a budget of $3000 a day. If the user spent any more money, the extra money would be automatically deducted from their savings account. In order to do this, I used Capitol One's Nessie api to simulate a real users bank account. The pebble ran a simple program that displayed the current budget remaining by grabbing data from the Api. In order to simulate the purchases of items, I created a mock store in C# with post request that updated the budgets of the current user after an item was bought. We created a custom skills for Amazon echo for this program too.

##Pictures
![Watch with budget displayed after buying a bed](BudgetThumbnail.png "Watch with budget displayed after buying a bed")
