import csv
import json

class DataLogger(object):
    def __init__(self):
        self.player_data = {"choice 1": {"correct_choice": 0, "incorrect_choice": 0, "correct_time": [], "incorrect_time": [], "trust": []},
                            "choice 2": {"correct_choice": 0, "incorrect_choice": 0, "correct_time": [], "incorrect_time": [],  "trust": []},
                            "choice 3": {"correct_choice": 0, "incorrect_choice": 0, "correct_time": [], "incorrect_time": [],  "trust": []},
                            "choice 4": {"correct_choice": 0, "incorrect_choice": 0, "correct_time": [], "incorrect_time": [],  "trust": []},
                            "choice 5": {"correct_choice": 0, "incorrect_choice": 0, "correct_time": [], "incorrect_time": [],  "trust": []},
                            "choice 6": {"correct_choice": 0, "incorrect_choice": 0, "correct_time": [], "incorrect_time": [], "trust": []},
                            "choice 7": {"correct_choice": 0, "incorrect_choice": 0, "correct_time": [], "incorrect_time": [],  "trust": []},
                            "choice 8": {"correct_choice": 0, "incorrect_choice": 0, "correct_time": [], "incorrect_time": [], "trust": []},
                            "path_ending": {"meet_jack": 0, "scene_3": 0, "meet_classmate": 0}}
        self.average_reading_time = 0.19
        with open('data.json', 'r') as file:
            data_list = json.load(file)  # Load the entire JSON array

            for data in data_list:
                name = data['name']
                value = data['value']

                dialogue, trust, choice_time = self.split_value(value)
                words = dialogue.split()
                word_count = len(words)
                choice_time = choice_time - (word_count*self.average_reading_time)
                if "I guess she’s an introvert" in dialogue:
                    self.player_data["choice 1"]["incorrect_choice"] += 1
                    self.player_data["choice 1"]["trust"].append(trust)
                    self.player_data["choice 1"]["incorrect_time"].append(choice_time)
                elif "Introvert" in dialogue:
                    self.player_data["choice 1"]["correct_choice"] += 1
                    self.player_data["choice 1"]["trust"].append(trust)
                    self.player_data["choice 1"]["correct_time"].append(choice_time)

                if "interesting thing to" in dialogue:
                    self.player_data["choice 2"]["correct_choice"] += 1
                    self.player_data["choice 2"]["trust"].append(trust)
                    self.player_data["choice 2"]["correct_time"].append(choice_time)
                if "good relationship" in dialogue:
                    self.player_data["choice 2"]["incorrect_choice"] += 1
                    self.player_data["choice 2"]["trust"].append(trust)
                    self.player_data["choice 2"]["incorrect_time"].append(choice_time)

                if "Speaking of" in dialogue:
                    self.player_data["choice 3"]["correct_choice"] += 1
                    self.player_data["choice 3"]["trust"].append(trust)
                    self.player_data["choice 3"]["correct_time"].append(choice_time)
                if "personal life" in dialogue:
                    self.player_data["choice 3"]["incorrect_choice"] += 1
                    self.player_data["choice 3"]["trust"].append(trust)
                    self.player_data["choice 3"]["incorrect_time"].append(choice_time)

                if "still be nice" in dialogue:
                    self.player_data["choice 4"]["correct_choice"] += 1
                    self.player_data["choice 4"]["trust"].append(trust)
                    self.player_data["choice 4"]["correct_time"].append(choice_time)
                if "a little weird" in dialogue:
                    self.player_data["choice 4"]["incorrect_choice"] += 1
                    self.player_data["choice 4"]["trust"].append(trust)
                    self.player_data["choice 4"]["incorrect_time"].append(choice_time)

                if "ruin our friendship" in dialogue:
                    self.player_data["choice 5"]["correct_choice"] += 1
                    self.player_data["choice 5"]["trust"].append(trust)
                    self.player_data["choice 5"]["correct_time"].append(choice_time)
                if "proves my point" in dialogue:
                    self.player_data["choice 5"]["incorrect_choice"] += 1
                    self.player_data["choice 5"]["trust"].append(trust)
                    self.player_data["choice 5"]["incorrect_time"].append(choice_time)

                if "a little concerning" in dialogue:
                    self.player_data["choice 6"]["correct_choice"] += 1
                    self.player_data["choice 6"]["trust"].append(trust)
                    self.player_data["choice 6"]["incorrect_time"].append(choice_time)
                if "trying to manipulate you" in dialogue:
                    if trust > 80:
                        self.player_data["path_ending"]["meet_jack"] += 1
                    elif trust > 65:
                        self.player_data["path_ending"]["scene_3"] += 1
                    else:
                        self.player_data["path_ending"]["meet_classmate"] += 1
                    self.player_data["choice 6"]["incorrect_choice"] += 1
                    self.player_data["choice 6"]["trust"].append(trust)
                    self.player_data["choice 6"]["correct_time"].append(choice_time)

                if "has he asked you to do things you know" in dialogue:
                    self.player_data["choice 7"]["correct_choice"] += 1
                    self.player_data["choice 7"]["trust"].append(trust)
                    self.player_data["choice 7"]["correct_time"].append(choice_time)
                if "I would be wary" in dialogue:
                    # if trust > 80:
                    #     self.player_data["path_ending"]["meet_jack"] += 1
                    # elif trust > 65:
                    #     self.player_data["path_ending"]["scene_3"] += 1
                    # else:
                    #     self.player_data["path_ending"]["meet_classmate"] += 1

                    self.player_data["choice 7"]["incorrect_choice"] += 1
                    self.player_data["choice 7"]["trust"].append(trust)
                    self.player_data["choice 7"]["incorrect_time"].append(choice_time)

                if "I'm always here for you if you decide" in dialogue or "Alright I won't force" in dialogue:
                    if trust > 80:
                        self.player_data["path_ending"]["meet_jack"] += 1
                    elif trust > 65:
                        self.player_data["path_ending"]["scene_3"] += 1
                    else:
                        self.player_data["path_ending"]["meet_classmate"] += 1
                    if "I'm always here" in dialogue:
                        self.player_data["choice 8"]["correct_choice"] += 1
                        self.player_data["choice 8"]["trust"].append(trust)
                        self.player_data["choice 8"]["correct_time"].append(choice_time)
                    else:
                        self.player_data["choice 8"]["incorrect_choice"] += 1
                        self.player_data["choice 8"]["trust"].append(trust)
                        self.player_data["choice 8"]["incorrect_time"].append(choice_time)
        print(self.player_data["path_ending"])
        self.save_player_data_to_csv("player_data")
        print("hello")



    def split_value(self, value_str):
        parts = value_str.split(" - ")
        dialogue = parts[0]
        trust_meter = int(parts[1].split()[0])
        choice_time_str = parts[1].split()[1].replace(',', '.')
        choice_time = float(choice_time_str)
        return dialogue, trust_meter, choice_time

    def save_player_data_to_csv(self, file_name):
        with open(file_name, mode='w', newline='') as file:
            fieldnames = ['Action', 'Correct Choice', 'Incorrect Choice', 'Trust', 'Correct Time', 'Incorrect Time']
            writer = csv.DictWriter(file, fieldnames=fieldnames)
            writer.writeheader()

            for action, data in self.player_data.items():
                #print(len(data["correct_time"]))
                if len(data.get('correct_time')) > 0:
                    #print('YESSSSSSSSSSSSSSSSSSSSSSS')
                    correct_time_avg = round(sum(data['correct_time']) / len(data['correct_time']),3)
                else:
                    correct_time_avg = 0
                if len(data['incorrect_time']) > 0:
                    #print('WOOP WOOP')
                    incorrect_time_avg = round(sum(data['incorrect_time']) / len(data['incorrect_time']),3)
                else:
                    incorrect_time_avg = 0
                row_data = {
                    'Action': action,
                    'Correct Choice': data.get('correct_choice', 0),
                    'Incorrect Choice': data.get('incorrect_choice', 0),
                    'Trust': data.get('trust', []),
                    'Correct Time': correct_time_avg,
                    'Incorrect Time': incorrect_time_avg
                }
                writer.writerow(row_data)

if __name__ == "__main__":
    DataLogger()
