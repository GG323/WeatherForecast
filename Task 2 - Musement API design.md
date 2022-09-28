# Task 2 - API design
 
## Task Description:
 
Now that we have a service that reads the forecast for a city, we want to save this info in the TUI Musement's API. 

Please provide the design for:
	endpoint/s to set the forecast for a specific city
	endpoint/s to read the forecast for a specific city
	
Please consider that we need to answer questions like :
	What's the weather in [city] today ?
	What will it be the weather in [city] tomorrow ?
	What was the weather in [city] on [day] ?
	
For each endpoint provide all required information: endpoint, payload, possible responses etc and description about the behavior.


## My solution:

### Comment:
If we plan to use external API, as a http://api.weatherapi.com/ , to get forecast information for a use in Musement API. We should strongly consider necessity of "SET forecast for a city" endpoint.If we plan to implement "SET forecast for a city" endpoint that would necessitate another application or service for getting forecast information from http://api.weatherapi.com/ or similar service and sending it to "SET forecast for a city" endpoint.
Better approach would be to integrate http://api.weatherapi.com/ in to Musement API's "GET forecast for a city" endpoints code base. That would allow us to get forecast information on demand, without developing system for saving this information, not to mention extra computing power and database storage space needed to hold up to date information.

If there is a good reason for developing Musement API this way, for example, to facilitate coding test to other job applicants, then scratch this comment.
So we know, I'm developing this documentation with belief that it's nothing to do with http://api.weatherapi.com/ .

#### "SET forecast for a city" - PUT /api/v3/cities/{cityId}{date}{forecast}

##### Description about the behavior:
Consumer should send cityId(unique identifier for a city), date(date in format of dd-MM-YYYY) and forecast(forecast text in English language).
Which we should save in data base, in new table or schema forecast(city_id, date, forecast_text) where city_id is foreign key to city entity.
If forecast exists for that city and for that date, then update forecast entity with new forecast_text.

##### Endpoint's documnetion:
```sh
Action: "put"
"summary": "Save forecast for specific city and for specific date"

"parameters":
	"$ref": "#/components/parameters/cityId"
	"$ref": "#/components/parameters/date"
	"$ref": "#/components/parameters/forecast"
API use specific "parameters" so there inclusion might be necessary, need to check that.
	"$ref": "#/components/parameters/X-Musement-Version"
	"$ref": "#/components/parameters/Accept-Language"
	
"requestBody": 
	"description": "Forecast for specific city and for specific date, update if already exists",
	"required": true,
	"content":
		"application/json": 
			"schema": 
				"$ref": "#/components/schemas/Forecast"

"responses":
	"200": {
		"description": "Returned when successful",
		"content": 
			"application/json": 
				"schema": 
					"type": "array",
					"items": 
						"$ref": "#/components/schemas/Forecast"
	"400": 
		"description": "Returned when payload data is incorrect"
	"403": 
		"description": "Returned when operation is not permitted"
	"404": 
		"description": "Returned when resource is not found" (city is not found)
	"503": 
		"description": "Returned when the service is unavailable"
```
API's current "security" settings for "put" requests, if meant for use in job applicant testing consider new role for testing tasks only
 ```sh
"security":
   "content_manager": 
       "activity:admin"
   "supplier": 
       "activity:admin"
```
#### "GET forecast for a city" - GET /api/v3/cities/{cityId}{date}

##### Description about the behavior:
Consumer's request specific city's forecast at specific date, so we should grab this information from previously mentioned table or schema forecast(city_id, date, forecast_text) and return it as forecast object with the same properties.

##### Endpoint's documnetion:
 ```sh
Action: "get"
"summary": "Get forecast for specific city and for specific date"

"parameters":
	"$ref": "#/components/parameters/cityId"
	"$ref": "#/components/parameters/date"
API use specific "parameters" so there inclusion might be necessary, need to check that.
	"$ref": "#/components/parameters/X-Musement-Version"
	"$ref": "#/components/parameters/Accept-Language"
	
"requestBody": 
	"description": "Forecast request for specific city and for specific date, update if already exists",
	"required": true,
	"content":
		"application/json": 
			"schema": 
				"$ref": "#/components/schemas/ForecastRequest"

"responses":
	"200": {
		"description": "Returned when successful",
		"content": 
			"application/json": 
				"schema": 
					"type": "array",
					"items": 
						"$ref": "#/components/schemas/Forecast"
	"400": 
		"description": "Returned when payload data is incorrect"
	"404": 
		"description": "Returned when resource is not found" (forecast is not found)
	"503": 
		"description": "Returned when the service is unavailable"
 ```
 