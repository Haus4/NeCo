Feature: Chat
	In Order to chat with other users
	as a user
	I want to have the possibility to chat with other users

@uc1chat
Scenario: Send message
	Given I am on the chat page
	And I have entered a chat message
	When I press the send button
	Then the result should be a message on the other users screen
