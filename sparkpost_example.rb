require 'net/http'
require 'JSON'
require 'rubygems'
require 'nokogiri'

def send_email(api_key, transmission_obj)
    uri = URI("https://api.sparkpost.com/api/v1/transmissions")
    req = Net::HTTP::Post.new(uri)
    req['Authorization'] = api_key
    req.body = transmission_obj.to_json
    
    res = Net::HTTP.start(uri.host, uri.port, :use_ssl => uri.scheme == 'https') {|http|
      http.request(req)
    }

    return res
end

# Initialize
api_key = ARGV[0]

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
subject_line = "Test Mailing from Ruby"
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
