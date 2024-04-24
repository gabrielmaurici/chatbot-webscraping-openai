const path = require('path');
const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');

async function getClientGrpc() {
  try {
    return new Promise((resolve) => {
      const protoFilePath = path.join(__dirname, '../protos/chatGpt.proto');
      const packageDefinition = protoLoader.loadSync(protoFilePath);
      const chatGptProto = grpc.loadPackageDefinition(packageDefinition).chatGpt;
      const client = new chatGptProto.ChatGpt('localhost:5100', grpc.credentials.createInsecure());
      resolve(client);
    });
  } catch (error) {
    throw error;
  }
}

module.exports = {
  getClientGrpc,
}