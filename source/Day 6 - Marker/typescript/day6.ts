const fs = require("fs") as typeof import("fs");
const input: string = fs.readFileSync("../input.txt", "utf8");

const readMarker = (input: string, amount: number) => {
  for (var i: number = 0; i < input.length; i++) {
    var group = input.slice(i, i + amount);

    if (group.length == new Set(group).size) {
      console.log("Marker:", i + amount);
      break;
    }
  }
};

readMarker(input, 14);
