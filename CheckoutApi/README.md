# Coding challenge

The purpose of this test is for you to demonstrate what you would consider to be high quality, production ready code. Spend as much (or little) time as you feel is necessary to demonstrate your opinion of production quality code. 

## Problem description
We’re going to see how far we can get in implementing a supermarket checkout API that calculates the total price of a number of items. 

In a normal supermarket, things are identified using Stock Keeping Units, or SKUs.  
In our store, we’ll use individual letters of the alphabet (A, B, C, D). Our goods are priced individually.  

In addition, some items are multipriced: buy _n_ of them, and they’ll cost you _y_ pounds.  
For example, item ‘A’ might cost 50 pounds individually, but this week we have a special offer: buy three ‘A’s and they’ll cost you 130. The price and offer table:

Item | Price | Offer
---|---|---
A | 50 | 3 for 130
B | 30 | 2 for 45
C | 20 | no offer
D | 15 | no offer

Our checkout accepts items in any order, so that if we scan a B, an A, and another B, we’ll recognize the two B’s and price them at 45 (for a total runnng price of 95).

## Advice on implementation
The API must be exposed as a RESTful service, which can be called using Postman or similar.  
The API should at least have one POST method, which will accept a string, representing the contents of a shopping basket.  
The response should include the total price.  

You are free to provide any accompanying materials that will assist us when testing your API.

## Example assertions

Here are a few unit test assertions to get you started:

Assert.That(0, Is.EqualTo(price_of("")))  
Assert.That(50, Is.EqualTo(price_of("A")))  
Assert.That(80, Is.EqualTo(price_of("AB")))  
Assert.That(115, Is.EqualTo(price_of("CDBA")))  
Assert.That(100, Is.EqualTo(price_of("AA")))  
Assert.That(130, Is.EqualTo(price_of("AAA")))  
Assert.That(175, Is.EqualTo(price_of("AAABB")))  

## Submission instructions
When you clone the repository, you will automatically check-out the `work` branch. Please commit & push all of your code to this branch.  When you feel you are ready - please make sure everything has successfully been pushed.

Once happy, please email petr.kunc@futurenet.com and hywel.rees@futurenet.com to inform them that you have pushed your work, and we will review at the earliest opportunity.
