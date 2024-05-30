module.exports = {
  extends: ["react-app", "react-app/jest", '../../eslint.cjs'],
  rules: {
  },
  module: "esnext",
  globals: {
    "ts-jest": {
        "useESM": true
    }
  }
};