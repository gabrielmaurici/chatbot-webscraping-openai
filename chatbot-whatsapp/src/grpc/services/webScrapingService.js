const path = require('path');
const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');

async function getLastMatch(team) {
  return new Promise((resolve, reject) => {
    const protoFilePath = path.join(__dirname, '../protos/webScraping.proto');
    console.log(protoFilePath);

    const packageDefinition = protoLoader.loadSync(protoFilePath);
    const webScrapingProto = grpc.loadPackageDefinition(packageDefinition).webScraping;

    const client = new webScrapingProto.WebScraping('localhost:5100', grpc.credentials.createInsecure());

    const request = {
      team: team 
    }

    client.GetLastMatch(request, (error, response) => {
      if (error) {
        return error;
      }
      resolve(response.lastMatch);
    });
  });
}

module.exports = { 
  getLastMatch
}