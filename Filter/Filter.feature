Feature: Filter

@Filter
Scenario: launch application and check filter section
Given launch application
When add filter section and input information
| Feild        | Operator    | Value         |
| Product Name | Is equal to | Aniseed Syrup |
Then check grid information