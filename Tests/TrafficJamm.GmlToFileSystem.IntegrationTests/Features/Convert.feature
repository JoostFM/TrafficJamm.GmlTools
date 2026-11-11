Feature: Calculator

Simple calculator for adding two numbers

@mytag
Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

	
Scenario: New Items in Source for an Existing Target Collection
	Given a xmldumpfile with ten DatabaseItems
	And ten corresponding items in the source folder
	And the five items already converted in the target folder
	When executed
	Then the five missing items are converted