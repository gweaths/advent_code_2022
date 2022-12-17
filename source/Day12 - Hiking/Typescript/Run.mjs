import { readFileSync } from "node:fs";

const lines = readFileSync("input.txt", { encoding: "utf-8" }) 
  .replace(/\r/g, "") // remove all \r characters to avoid issues on Windows
  .trim() // Remove starting/ending whitespace
  .split("\n"); // Split on newline

const getInput = () => {
  const res = {
    start: {},
    end: {},
    map: [],
  };

  res.map = lines.map((line, y) =>
    [...line].map((value, x) => {
      if (value === "S") {
        res.start = {
          y,
          x,
        };
        return 0;
      }
      if (value === "E") {
        res.end = { y, x };
        return 25;
      }
      return value.charCodeAt(0) - "a".charCodeAt(0);
    })
  );
  return res;
};

const pointToInt = (x, y) => {
  //divide by 1000 to get smaller numbers
  return y * 1000 + x;
};

const intToPoint = (int) => {
  return {
    y: Math.floor(int / 1000),
    x: int % 1000,
  };
};

const getNeighbors = (x, y, map) => {
  const res = [];
  if (y + 1 < map.length && map[y + 1][x] <= map[y][x] + 1) {
    res.push(pointToInt(x, y + 1));
  }
  if (y - 1 >= 0 && map[y - 1][x] <= map[y][x] + 1) {
    res.push(pointToInt(x, y - 1));
  }
  if (x + 1 < map[y].length && map[y][x + 1] <= map[y][x] + 1) {
    res.push(pointToInt(x + 1, y));
  }
  if (x - 1 >= 0 && map[y][x - 1] <= map[y][x] + 1) {
    res.push(pointToInt(x - 1, y));
  }
  return res;
};

const applyAlgorithm = (map, start, end) => {
  const distance = {};
  const previous = {};
  let queue = [];
  for (let y = 0; y < map.length; y++) {
    for (let x = 0; x < map[y].length; x++) {
      const id = pointToInt(x, y);
      distance[id] = Infinity;
      queue.push(id);
    }
  }
  distance[pointToInt(start.x, start.y)] = 0;

  while (queue.length) {
    let u = null;
    for (const current of queue) {
      if (u === null || distance[current] < distance[u]) {
        u = current;
      }
    }
    if (u === pointToInt(end.x, end.y)) {
      break;
    }
    queue = queue.filter((x) => x !== u);

    const point = intToPoint(u);
    const neighbors = getNeighbors(point.x, point.y, map);
    for (const v of neighbors) {
      if (queue.includes(v)) {
        const alt = distance[u] + 1;
        if (alt < distance[v]) {
          distance[v] = alt;
          previous[v] = u;
        }
      }
    }
  }
  return {
    dist: distance,
    prev: previous,
  };
};

const part1 = () => {
  const input = getInput();
  const data = applyAlgorithm(input.map, input.start, input.end);
  const distance = data.dist[pointToInt(input.end.x, input.end.y)];
  console.log(distance);
};

// --- Part 2 ----
const getNeibours_part2 = (x, y, map) => {
  const res = [];
  if (y + 1 < map.length && map[y + 1][x] >= map[y][x] - 1) {
    res.push(pointToInt(x, y + 1));
  }
  if (y - 1 >= 0 && map[y - 1][x] >= map[y][x] - 1) {
    res.push(pointToInt(x, y - 1));
  }
  if (x + 1 < map[y].length && map[y][x + 1] >= map[y][x] - 1) {
    res.push(pointToInt(x + 1, y));
  }
  if (x - 1 >= 0 && map[y][x - 1] >= map[y][x] - 1) {
    res.push(pointToInt(x - 1, y));
  }
  return res;
};

const applyAlgorithm_part2 = (map, start) => {
  const dist = {};
  const prev = {};
  let queue = [];
  for (let y = 0; y < map.length; y++) {
    for (let x = 0; x < map[y].length; x++) {
      const id = pointToInt(x, y);
      dist[id] = Infinity;
      queue.push(id);
    }
  }

  dist[pointToInt(start.x, start.y)] = 0;

  while (queue.length) {
    let u = null;
    for (const current of queue) {
      if (u === null || dist[current] < dist[u]) {
        u = current;
      }
    }
    const point = intToPoint(u);
    if (map[point.y][point.x] === 0) {
      return dist[u];
    }
    queue = queue.filter((x) => x !== u);

    const neighbors = getNeibours_part2(point.x, point.y, map);
    for (const v of neighbors) {
      if (queue.includes(v)) {
        const alt = dist[u] + 1;
        if (alt < dist[v]) {
          dist[v] = alt;
          prev[v] = u;
        }
      }
    }
  }
};

function part2() {
  const input = getInput();
  const distance = applyAlgorithm_part2(input.map, input.end);
  console.log(distance);
}

part1();
part2();
