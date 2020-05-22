import sys
from entities.client import Client
from PyInquirer import prompt
from ui import menu_template
from ui.ui import UI

client = Client()
username = prompt(menu_template.input_name)['value']
if client.is_exists(username):
    client.connect(username)
else:
    if len(sys.argv) > 1 and sys.argv[1] == 'owner':
        client.add_owner(username)
    elif len(sys.argv) > 1 and sys.argv[1] == 'admin':
        client.add_admin(username)
    else:
        client.add_user(username)
    client.connect(username)

ui = UI(username)
ui.start()
