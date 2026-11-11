Feature: Convertor

Scenario: New and modified items in source for an existing target collection
	Given the GML dump is staged
	And the source folder contains all nine matching audio files
	And six tracks already exist in the target collection
	When conversion is executed
	Then the three missing tracks and the modified track are transferred
