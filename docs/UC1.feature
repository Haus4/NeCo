Feature: Chat
	In Order to chat with other users
	As a user
	I want to have the possibility to chat with other users

@uc1chat
Scenario: Chat with another user
	Given Two users are on the chat page of the app
	And I have entered a chat message
	When I press the send button
	Then the result should be a message on the other users screen

	
