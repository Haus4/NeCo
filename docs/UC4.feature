Feature: Share Files
	In Order to share files with other users
	as a user
	I want to have the possibility to share files with other users

@uc4chat
Scenario: Send files
	Given Two users are on the chat page of the app
	And I have choosen a file to share
	When I press the send button
	Then the result should be a file on the other users screen
	

Scenario: Receive files
	Given Two users are on the chat page of the app
	And another user has choosen a file to share
	When another user has pressed the send button
	Then the result should be a file on my screen


Scenario: Session connect failed
	Given I have no connection to the internet
	And the server has an error
	When I try to connect to the server
	Then the result should be an error message on my screen according to the error
	
	
