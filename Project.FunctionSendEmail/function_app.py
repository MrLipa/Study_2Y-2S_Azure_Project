import logging
import azure.functions as func
from EmailSender import EmailSender
import json

app = func.FunctionApp()

@app.event_grid_trigger(arg_name="azeventgrid")
def EventGridTrigger(azeventgrid: func.EventGridEvent):
    try:
        data = azeventgrid.get_json()
    except ValueError:
        logging.info('Error processing event grid data.')
        return

    email = data.get('email')
    subject = data.get('subject')
    message = data.get('message')

    logging.info(f'{email} {subject} {message}')

    email_sender = EmailSender("carlie44@ethereal.email", 
                               "8fUAYpt3upPGcUwUEn", 
                               "smtp.ethereal.email", 
                               587)
    
    if email and subject and message:
        success = email_sender.send_email(email, subject, message)
        if success:
            logging.info('Email sent successfully.')
        else:
            logging.info('Failed to send email.')
    else:
        logging.info('Email, subject, or message is missing.')