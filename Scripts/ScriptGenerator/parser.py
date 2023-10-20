import json

lines = []

with open("/Users/santiagovira/Documents/Unity Projects/HumanTraffickingSimulator/Assets/Scripts/ScriptGenerator/texttoparse.txt") as f:
    lines = [[(split := line.split(":"))[0], ":".join(split[1:]).strip()]
             for line in f.readlines()]

data = {"messages": []}


def msg(pe, t, i, pa, ch):
    new_msg = {
        "person": pe,
        "text": t,
        "id": i,
        "parent": pa,
        "children": ch
    }

    if len(data["messages"]) > 0:
        print(i)
        data["messages"][pa - i]["children"].append(i)

    data["messages"].append(new_msg)


for line in lines:
    id = len(data["messages"])

    if "//" in line[1]:
        [text1, text2] = line[1].split("//")
        msg(line[0], text1, id, id-1, [id + 2])
        print(data["messages"])
        msg(line[0], text2, id + 1, id-1, [])
        print(data["messages"])

    else:
        # Parent is automatically previous message
        msg(line[0], line[1], id, id-1, [])
        print(data["messages"])


with open("/Users/santiagovira/Documents/Unity Projects/HumanTraffickingSimulator/Assets/Scripts/ScriptGenerator/scene2.json", "w") as f:
    json.dump(data, f)
