Feature: Manage Profile
	In Order to manage my profile in the application
	as a user
	I want to have the possibility change certain options in my profile

@uc5manageprofle
Scenario: Change picture
	Given The user is on the main site of the app
	And navigates to the profile site
	When he tap on the change-picture button
	Then a dialog for choosing a picture should appear
	When he confirms the file
	Then the profile picture should have changed

Scenario: Change nickname
	Given The user is on the main site of the app
	And navigates to the profile site
	When he tap on the change-nickname button
	Then a dialog for choosing a nickname should appear
	When he confirms the nickname
	Then the nickname should have changed

	
	
