Feature: Send message
	In Order to send a message to another user nearby 
	as a user
	I want to have the possibility to send a message to another user nearby

@uc1chat
Scenario: Send message
	Given Two users are on the chat page of the app
	And I have entered a chat message
	When I press the send button
	And the user is within a certain distance to me
	Then the result should be a message on the other users screen

Scenario: Session connect failed
	Given I have no connection to the internet
	And the server has an error
	When I try to connect to the server
	Then the result should be an error message on my screen according to the error
	
