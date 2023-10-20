import json

first, second = {}, {}

with open("/Users/santiagovira/Documents/Unity Projects/HumanTraffickingSimulator/Assets/Scripts/ScriptGenerator/firsthalf.json") as f:
    first = json.load(f)

with open("/Users/santiagovira/Documents/Unity Projects/HumanTraffickingSimulator/Assets/Scripts/ScriptGenerator/secondhalf.json") as f:
    second = json.load(f)

data = first
id_diff = data["messages"][-1]["id"]

for msg in second["messages"]:
    id = msg["id"] + id_diff + 1
    parent = id_diff if msg["parent"] == -1 else msg["parent"] + id_diff + 1
    children = [id_num + id_diff + 1 for id_num in msg["children"]]

    data["messages"].append({
        "person": msg["person"],
        "text": msg["text"],

        "id": id,
        "parent": parent,
        "children": children,
    })

with open("/Users/santiagovira/Documents/Unity Projects/HumanTraffickingSimulator/Assets/Scripts/ScriptGenerator/script.json", "w") as f:
    json.dump(data, f)
