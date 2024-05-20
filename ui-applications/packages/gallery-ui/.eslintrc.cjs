module.exports = {
  extends: ["react-app", "react-app/jest", '../../eslint.cjs'],
  env: { browser: true, es2020: true,  "jest/globals": true },
  globals: {
    "ts-jest": {
        "useESM": true
    }
  },
  rules: {
  },
  module: "esnext",
};