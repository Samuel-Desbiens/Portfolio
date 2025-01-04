module.exports = {
  publicPath:
    process.env.NODE_ENV === 'production' ?
    '/Tp3AppWeb/'
    : '/',
    configureWebpack: {
    devtool: 'source-map'
  }
}
