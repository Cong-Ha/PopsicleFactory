# Popsicle Factory Exercise

## Prerequisites

- .NET 8

## Background
Popsicle Co., a very successful Popsicle factory, would like to come out of the stone age by bringing their store to the  Web. In order to support multiple clients they need a RESTFUL web service to manage their Popsicle inventory.  

## Narrative
**As** Popsicle Co., **I want** a webservice that will manage the CRUD operations of our Popsicles, **so that** our inventory of Popsicles can be managed from the Web.

## Promises Expected
- Popsicle requests should be valid
- Popsicles should exist for Update, Delete and Read operations

## Promises Made
For this task, you will implement the create, read, and update operations.
- Create Popsicles
- Read Popsicles
- Update Popsicles

## Scenarios

### Scenario: Promise Broken - Popsicle Request is Invalid
**Given** a Popsicle request is invalid

**When** the request is received from the web service

**Then** an appropriate status code will be returned

**And** an error message explaining what was wrong should be included in the payload.

### Scenario: Promise Broken - Popsicle does not exist
**Given** a Popsicle request is valid

**And** the Popsicle requested does not exist

**When** the request is received from the web service

**Then** an appropriate status code will be returned

**And** the message explaining that the Popsicle does not exist should be returned.

### Scenario: Create Popsicle
**Given** the Popsicle request is valid

**When** the create request is received from the web service

**Then** an appropriate status code will be returned

**And** the Popsicle will be persisted

**And** a view model of the Popsicle will be returned.

### Scenario: Replace Popsicle
**Given** the Popsicle request is valid

**And** a Popsicle exists

**When** the replace request is received from the web service

**Then** an appropriate status code will be returned

**And** the Popsicle will be persisted with all properties overwritten

**And** a view model of the Popsicle will be returned.

### Scenario: Get Popsicle
**Given** the Popsicle request is valid

**And** a Popsicle exists

**When** the get request is received from the web service

**Then** an appropriate status code will be returned

**And** the Popsicle View Model will be returned.

### Scenario: Search Popsicles
**Given** the Popsicle request is valid

**And** Popsicles exist that match the search criteria

**When** the search request is received from the web service

**Then** an appropriate status code will be returned

**And** the payload should contain the list of Popsicles that matched the search criteria.

## Requirements
- Create popsicle code should be unit tested (Read and update operations do not need to be unit tested)
- Project should be able to run locally
- Examples should be included for calling the Webservice. (IE: Swagger, Postman, Curl)
- Exercise should take no longer than 6 hours

## Design Considerations
- Feel free to mock the backend
- Feel free to split the code into as many projects as you like