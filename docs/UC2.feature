Feature: Receive message
	In Order to receive a message from another user nearby 
	as a user
	I want to have the possibility to receive a message from other users nearby

@uc2chat
Scenario: Receive message
	Given Two users are on the chat page of the app
	And I am within a certain distance to the other user
	When another user has send a chat message to me
	And the user is within a certain distance to me
	Then the result should be a message on the my screen

Scenario: Session connect failed
	Given I have no connection to the internet
	And the server has an error
	When I try to connect to the server
	Then the result should be an error message on my screen according to the error
	
	
