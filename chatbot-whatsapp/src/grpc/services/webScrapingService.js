const path = require('path');
const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');

async function getClientGrpc() {
  return new Promise((resolve, reject) => {
    const protoFilePath = path.join(__dirname, '../protos/webScraping.proto');
    const packageDefinition = protoLoader.loadSync(protoFilePath);
    const webScrapingProto = grpc.loadPackageDefinition(packageDefinition).webScraping;
    const client = new webScrapingProto.WebScraping('localhost:5100', grpc.credentials.createInsecure());
    resolve(client);
  })
}

async function getLastMatch(team) {
  const client = await getClientGrpc();
  return new Promise((resolve, reject) => {
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

async function getNextMatch(team) {
  const client = await getClientGrpc();

  return new Promise((resolve, reject) => {
    const request = {
      team: team 
    }
    client.GetNextMatch(request, (error, response) => {
      if (error) {
        return error;
      }
      resolve(response.nextMatch);
    });
  });
}

module.exports = { 
  getLastMatch,
  getNextMatch
}