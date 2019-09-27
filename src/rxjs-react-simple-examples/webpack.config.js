// Note this only includes basic configuration for development mode.
// For a more comprehensive configuration check:
// https://github.com/fable-compiler/webpack-config-template

var path = require("path");

module.exports = {
    mode: "development",
    entry: "./src/index.jsx",
    output: {
        path: path.join(__dirname, "./dist"),
        filename: "rxjs-react-simple-tests.js"
    },
    module: {
        rules: [{
            test: /\.m?jsx?$/,
            exclude: /(node_modules|bower_components)/,
            use: {
                loader: 'babel-loader',
                options: {
                  presets: ['@babel/preset-env', '@babel/react']
                }
            }
        }]
    }
}