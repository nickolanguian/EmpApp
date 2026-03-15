Feature: Employee feature
To validate the Employee page

@employee
Scenario Outline: Validate creation of employee
	Given I navigate to the URL
	When I click the Login link
	Then I Enter the credentials as <username> and <password>
	And I click the login button
	Then I click the Employees Link
	Then I click the New Employees button
	And I create an Employee using the following details:
		| FullName | Age | MonthlySalary | DurationWorked | Grade   | EmailAddress     |
		| John Doe |  30 |          5000 |              5 | C-Level | johndoe@test.com |

Examples: 
| username | password |
| admin    | password |