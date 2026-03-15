Feature: Login feature

To Test the login page

@loginpass
Scenario Outline: Login with valid admin credentials
	Given I navigate to the URL
	When I click the Login link
	Then I Enter the credentials as <username> and <password>
	And I click the login button
	Then I validate that the admin was logged in successfully

Examples: 
| username | password |
| admin    | password |

@loginfail
Scenario Outline: Login with invalid admin credentials
	Given I navigate to the URL
	When I click the Login link
	Then I Enter the credentials as <username> and <password>
	And I click the login button
	Then I validate that the user was not logged in

Examples: 
| username | password |
| admin1   | password |
