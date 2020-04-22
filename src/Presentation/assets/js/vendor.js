import 'jquery';
import _ from 'lodash';
import '@babel/polyfill';
import 'bootstrap';
import 'parsleyjs';

global._ = _;
global.$ = global.jQuery = require('jquery');

/**Select2 */
global.select2 = require("select2");

$(document).ready(function () {
    $('.select2-single').select2({
        minimumResultsForSearch: -1
    });
});
