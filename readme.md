# Advice Slip Middleman API

This is a piece of software created as a test/excercise and my intro into ASP.NET.

Is is a simple API that draws data from the [Advice Slip API](https://api.adviceslip.com/) , and presents it in a more palatable form.

## Usage

### API

```
https://localhost:5001/api/
Returns a JSON with a random advice slip

https://localhost:5001/api/9
Returns a JSON with an ID of 9
As of time of writing this, there are 224 unique slips, with the first slip having an ID of 1.

https://localhost:5001/api/search/and
Searches for advice slips containing the string "and" and returns the result.
```

Invalid searches and ID will return a proper message JSON, in accordance to the original API(refer to the original [documentation](https://api.adviceslip.com/#messages)).

Internal errors should also produce a proper JSON message in the following format:
```
{
  "contentType": "application/json",
  "serializerSettings": null,
  "statusCode": 200,
  "value": {
    "message": {
      "type": "Internal Error",
      "text": "{exception} Unable to deserialize {response}"
    }
  }
}
```
### Testpages:
There are 2 minimal webpages included in the project  for testing purposes.
```
https://localhost:5001/index
Provides random advice.
https://localhost:5001/search
Provides an elementary search interface.
```

## Further reading

For further information please refer to the design doc.

## Updates

Project updates are expected to arrive in the near-ish future