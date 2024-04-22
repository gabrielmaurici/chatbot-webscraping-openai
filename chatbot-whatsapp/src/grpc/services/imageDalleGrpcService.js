const path = require('path');
const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');

async function getClientGrpc() {
  try {
    return new Promise((resolve, reject) => {
      const protoFilePath = path.join(__dirname, '../protos/imageDalle.proto');
      const packageDefinition = protoLoader.loadSync(protoFilePath);
      const imageDalleProto = grpc.loadPackageDefinition(packageDefinition).imageDalle;
      const client = new imageDalleProto.ImageDalle('localhost:5100', grpc.credentials.createInsecure());
      resolve(client);
    });
  } catch (error) {
    throw error;
  }
}

module.exports = {
  getClientGrpc
}