Feature: Chat encrypted
	In Order to chat with other users encrypted
	as a user
	I want to have the possibility to chat with another users encrypted

@uc3chat
Scenario: Send encrypted message
	Given Two users are on the chat page of the app
	And I have entered a chat message
	When I press the send button
	Then the message should get encrypted
	And the result should be a message on the other users screen
	And the message should be sent encrypted

Scenario: Receive encrypted message
	Given Two users are on the chat page of the app
	When another user has send a chat message to me
	Then the encrypted message should get dencrypted
	And the result should be a message on the my screen
	And the message should be sent encrypted

Scenario: Session connect failed
	Given I have no connection to the internet
	And the server has an error
	When I try to connect to the server
	Then the result should be an error message on my screen according to the error
	
	
