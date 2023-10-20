import json
import os

data = {"messages": []}


with open("./script.json", "r+") as f:
    print(os.path.abspath("./script.json"))
    json.dump(data, f)


def quote(person: str, text: str, parent: int) -> None:
    id = len(data)
    data.append({"person": "Jack" if person == "j" else "You" if person == "s" else "Sys", "text": text,
                "parent": parent, "id": id, "children": []})

    if parent >= 0:
        data[parent]["children"].append(id)


while True:
    for obj in data:
        print(f"{obj['id']}: {obj['text'][:15]}")

    print()
    cmd = input("> ")
    if cmd == "quit":
        break
    elif cmd == "del":
        data.pop(-1)
        continue

    person = input("Who is saying this? (y/j/s) ")
    text = input("Message: ")
    parent = input("ID # of parent: ")
    if parent == "p":
        parent = len(data) - 1
    else:
        parent = int(parent)

    quote(person, text, parent)


with open("/Users/santiagovira/Documents/Unity Projects/HumanTraffickingSimulator/Assets/Scripts/ScriptGenerator/scene2.json", "w") as f:
    json.dump(data, f)
