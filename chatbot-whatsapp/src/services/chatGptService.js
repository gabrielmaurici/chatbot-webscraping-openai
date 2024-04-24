const { error } = require('qrcode-terminal');
const chatGptGrpcService = require('../grpc/services/chatGptGrpcService');

async function checkIfMessageRequestsAskQuestionsIA(message) {
    try {
      if (message.startsWith("!IA-chat")) {
        return await AskQuestionsIA(message);
      }
      return undefined;
    } catch (error) {
      return error;
    }
}

async function AskQuestionsIA(ask) {
    const client = await chatGptGrpcService.getClientGrpc();
    return new Promise((resolve, reject) => {
      const request = { ask: ask };
      client.AskQuestion(request, (error, response) => {
        if (error) {
          reject("Ocorreu algum erro ao realizar a pergunta para a IA: " + error);
          return;
        }
        resolve(response.responseIA);
      });
    })
    
}

module.exports = {
  checkIfMessageRequestsAskQuestionsIA
}