var Converter = require('openapi-to-postmanv2');
var fs = require('fs');

data = { type: 'file', data: 'c:\\openapi\\swaggerdoc-openapi.json' };
options = {
    schemaFaker: true,
    requestNameSource: 'fallback',
    indentCharacter: ' '
};
callback = function (err, result) {
    console.log("err:");
    console.log(err);
    console.log("result:");
    console.log(result);
    console.log("result.output[0].data:");
    console.log(result.output[0].data);
    fs.writeFileSync('c:\\openapi\\swaggerdoc-collection.json', JSON.stringify(result.output[0].data));
};

Converter.convert(data, options, callback);