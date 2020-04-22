const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const autoprefixer = require("autoprefixer");
const StyleLintPlugin = require('stylelint-webpack-plugin');

const ROOT = path.resolve(__dirname);

console.log('@@@@@@@@@ USING DEVELOPMENT @@@@@@@@@@@@@@@');

var config = (env, options) => {
    let isProduction;

    if (env && env.NODE_ENV && env.NODE_ENV !== 'development') {
        isProduction = true;
    } else if (options && options.mode === 'production') {
        isProduction = true;
    } else {
        isProduction = false;
    }

    return {
        devtool: isProduction ? "source-map" : "cheap-module-eval-source-map",
        stats: {
            warnings: isProduction,
        },
        performance: {
            hints: false
        },
        resolve: {
            extensions: ['.js', '.jsx', '.ts', '.tsx']
        },
        entry: {
            vendor: path.join(ROOT, '/assets/js/vendor.js'),
            app: path.join(ROOT, '/assets/js/app.js'),
            style: path.join(ROOT, '/assets/scss/app.scss'),

            adminStyle: path.join(ROOT, '/assets/scss/admin/app.scss'),
            adminJS: path.join(ROOT, '/assets/js/admin/app.js'),
        },
        output: {
            path: ROOT + '/wwwroot/',
            filename: isProduction ? 'assets/js/[name].bundle.js' : 'assets/js-dev/[name].bundle.js',
            chunkFilename: isProduction ? 'assets/js/[id].chunk.js' : 'assets/js-dev/[id].chunk.js',
            publicPath: '/'
        },
        module: {
            rules: [
                {
                    test: /\.(sa|sc|c)ss$/,
                    use: [
                        MiniCssExtractPlugin.loader,
                        {
                            loader: "css-loader",
                            options: {
                                url: true,
                                importLoaders: 2,
                                sourceMap: true,
                                publicPath: "/assets",
                            }
                        },
                        {
                            loader: require.resolve("postcss-loader"),
                            options: {
                                ident: "postcss",
                                plugins: () => [
                                    autoprefixer({
                                        browsers: [
                                            ">1%",
                                            "last 4 versions",
                                            "Firefox ESR",
                                            "not ie < 9" // React doesn't support IE8 anyway
                                        ],
                                        flexbox: "no-2009"
                                    })
                                ]
                            }
                        },
                        {
                            loader: 'sass-loader',
                            options: {
                                sourceMap: true,
                                publicPath: "/assets",
                            }
                        },
                    ]
                },
                {
                    test: /\.js(x)?$/,
                    exclude: /node_modules/,
                    use: {
                        loader: 'babel-loader',
                    }
                },
                {
                    test: /\.js(x)?$/,
                    enforce: 'pre',
                    exclude: /(node_modules|wwwroot)/,
                    use: {
                        loader: 'eslint-loader',
                        options: {
                            configFile: path.resolve(ROOT, '.eslintrc')
                        },
                    }
                },
                {
                    test: /\.(png|jpg|jpeg|gif|svg|ttf|otf|woff|woff2|eot)$/,
                    loader: 'url-loader?limit=4096',
                    options: {
                        publicPath: '/assets',
                        name: '[name].[ext]',
                        outputPath: path.join(ROOT, '/wwwroot/images')
                    }
                }
            ]
        },
        plugins: [
            function () {
                this.plugin('watch-run', function (watching, callback) {
                    console.log(
                        '\x1b[33m%s\x1b[0m',
                        `Begin compile at ${new Date().toTimeString()}`
                    );
                    callback();
                });
            },
            new MiniCssExtractPlugin({
                publicPath: '/assets',
                filename: isProduction ? 'assets/css/[name].css' : 'assets/css-dev/[name].css'
            }),
            new StyleLintPlugin({
                configFile: path.resolve(ROOT, '.stylelintrc'),
                context: path.join(ROOT, "/assets"),
                files: '**/*.scss',
                failOnError: false,
                quiet: false,
            })
        ],
    }
};

module.exports = config;
