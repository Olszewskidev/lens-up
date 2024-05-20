const { ESLint } = require("eslint");
const { TsCompiler } = require("ts-jest");

module.exports = {
    root: true,
    env: { browser: true, es2020: true,  "jest/globals": true },
    globals: {
        "ts-jest": {
            "useESM": true
        }
      },
    extends: [
        'eslint:recommended',
        'plugin:@typescript-eslint/recommended',
        'plugin:react-hooks/recommended',
        "react-app", "react-app/jest"
    ],
    module: "esnext",
    ignorePatterns: ['dist', '.eslintrc.cjs'],
    parser: '@typescript-eslint/parser',
    plugins: ['react-refresh'],
    rules: {
        'react-refresh/only-export-components': [
            'warn',
            { allowConstantExport: true },
        ],
    }
}
