const { Client, LocalAuth } = require('whatsapp-web.js');
const qrcode = require('qrcode-terminal');
const webScrapingService = require('./grpc/services/webScrapingService');

const client = new Client({
    authStrategy: new LocalAuth(),
    webVersionCache: {
        type: 'remote',
        remotePath: 'https://raw.githubusercontent.com/wppconnect-team/wa-version/main/html/2.2409.0.html',
    }
});

client.once('ready', () => {
    console.log('Client is ready!');
});

client.on('qr', (qr) => {
    qrcode.generate(qr, {small: true});
});

client.on('message_create', message => {
    if (message.body === "Flu") {
        getLastMatch(message, "Fluminense");
    }
    if (message.body === "Fla") {
        getLastMatch(message, "Flamengo");
    }
    if (message.body === "Brusque") {
        getLastMatch(message, "Brusque");
    }
});

async function getLastMatch(message, team) {
    try {
        let lastMatch = await webScrapingService.getLastMatch(team);
        message.reply(lastMatch)
        
    } catch (error) {
        message.reply("Ocorreu algum erro ao obter a última partida: " + error);
    }
}

client.initialize();
