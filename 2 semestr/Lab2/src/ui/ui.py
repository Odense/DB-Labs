from entities.client import Client
from PyInquirer import prompt
from ui import menu_template
from termcolor import colored


class UI:
    client = Client()
    menu_type = None

    def __init__(self, client_name):
        self.client.username = client_name
        if self.client.is_admin(client_name):
            self.menu_type = menu_template.admin_ui
        elif self.client.is_owner(client_name):
            self.menu_type = menu_template.owner_ui
        elif self.client.is_common_user(client_name):
            self.menu_type = menu_template.common_ui

    def start(self):
        while True:
            op = prompt(self.menu_type)['operation']
            if op == 'SEND MESSAGE':
                message = prompt(menu_template.input_message)['value']
                receiver = prompt(menu_template.input_username)['value']
                if self.client.is_exists(receiver):
                    self.client.create_message(message, receiver)
                else:
                    print(colored('Unable send message to {}: user does not exist'.format(receiver), 'red'))
            if op == 'INBOX':
                messages = self.client.get_inbox(self.client.username, 10)
                if messages:
                    message_list = []
                    for hashcode in messages:
                        mess = self.client.get_message(hashcode)
                        message_dict = {'message_str': mess['Sender'] + ' ' + mess['Message'][:8] + '...',
                                        'hashcode': hashcode}
                        message_list.append(message_dict)
                    hashcode_new = prompt(menu_template.choose_message(message_list))['value']
                    print(self.client.get_message(hashcode_new)['Message'])
                    self.client.storage_manager.update_message_status(hashcode_new, 'RECEIVED')
                else:
                    print(colored('No messages', 'yellow'))
            if op == 'GET SPAMMERS RARING':
                spammers = self.client.get_spammers(10)
                for spammer in spammers:
                    print(colored(spammer[0] + ': {} '.format(int(spammer[1])) + 'spam messages', 'red'))
            if op == 'GET ONLINE USERS':
                print(self.client.get_online())
            if op == 'PROMOTE TO ADMIN':
                username = prompt(menu_template.input_username)['value']
                if self.client.is_admin(username) or self.client.is_owner(username):
                    print(colored("User {} is already admin".format(username), 'red'))
                else:
                    self.client.promote_to_admin(username)
            if op == 'DEMOTE TO USUAL USER':
                username = prompt(menu_template.input_username)['value']
                if self.client.is_common_user(username):
                    print(colored("User {} is already common user".format(username), 'red'))
                else:
                    self.client.demote_to_user(username)
            if op == 'EXIT':
                self.client.disconnect()
                quit()
