import azure.functions as func
import logging
from MealOptimizer import MealOptimizer
import json
from azure.eventgrid import EventGridPublisherClient, EventGridEvent
from azure.core.credentials import AzureKeyCredential

app = func.FunctionApp(http_auth_level=func.AuthLevel.ANONYMOUS)

@app.route(route="optimize_meal", methods=['POST'])
def http_trigger(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    try:
        data = req.get_json()
    except ValueError:
        return func.HttpResponse("Invalid JSON", status_code=400)
    
    print(data)

    products = data.get('products')
    limits = data.get('limits')

    if not products or not limits:
        return func.httpresponse(
            "please provide both products and limits in the json payload.",
            status_code=400
        )
    
    optimizer = MealOptimizer(products, limits)
    optimized_products = optimizer.optimize_meal()

    # send_event_to_event_grid(optimized_products)

    return func.HttpResponse(json.dumps(optimized_products), status_code=200, mimetype="application/json")

def send_event_to_event_grid(data):
    topic_endpoint = ""
    topic_key = ""

    credential = AzureKeyCredential(topic_key)
    client = EventGridPublisherClient(topic_endpoint, credential)

    event = EventGridEvent(
        subject="optimize_meal",
        event_type="OptimizeMeal",
        data_version="1.0",
        data=data
    )

    try:
        client.send(event)
        logging.info("Event sent to Event Grid")
    except Exception as e:
        logging.error(f"Error sending event: {e}")