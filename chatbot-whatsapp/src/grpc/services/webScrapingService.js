const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');

async function getLastMatch() {
  return new Promise((resolve, reject) => {
    const packageDefinition = protoLoader.loadSync('/home/gabriel/projetos/chatbot-webscraping-chatgpt/chatbot-whatsapp/src/grpc/protos/webScraping.proto');
    const webScrapingProto = grpc.loadPackageDefinition(packageDefinition).webScraping;

    const client = new webScrapingProto.WebScraping('localhost:5100', grpc.credentials.createInsecure());

    client.GetLastMatch({}, (error, response) => {
      if (error) {
        reject(error);
        return;
      }
      resolve(response.lastMatch);
    });
  });
}

module.exports = { 
    getLastMatch
}