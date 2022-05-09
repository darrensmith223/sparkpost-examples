import requests
import os

def send_email(api_key, transmission_obj):
    uri = "https://api.sparkpost.com/api/v1/transmissions"
    headers = {
        "Authorization": api_key
    }

    res = requests.post(uri, json=transmission_obj, headers=headers)

    return res


# Initialize
api_key = os.environ.get("API_KEY")

# Define email elements
campaign_id = "test-campaign"
recipient_obj = {
    "address": {
        "email": "test@example.com",
        "name": "First Name"
    }
}
from_address_obj = {
    "email": "noreply@example.com",
    "name": "No Reply"
}
subject_line = "Test Mailing from Python"
body_html = "<html><body><p>this is a test</p></body></html>"


# Compile object for Transmission API
transmission_obj = {
    "campaign_id": campaign_id,
    "recipients": [
        recipient_obj
    ],
    "content": {
        "from": from_address_obj,
        "subject": subject_line,
        "html": body_html
    }
}

# Send Email
response = send_email(api_key, transmission_obj)
print(response.text)
