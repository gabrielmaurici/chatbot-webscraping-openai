const { Client, LocalAuth } = require('whatsapp-web.js');
const qrcode = require('qrcode-terminal');
const webScrapingService = require('./grpc/services/webScrapingService')

const client = new Client({
    authStrategy: new LocalAuth()
});

client.once('ready', () => {
    console.log('Client is ready!');
});

client.on('qr', (qr) => {
    qrcode.generate(qr, {small: true});
});

client.on('message_create', message => {
    if (message.body === "Flu") {
        getLastMatchFlu(message);
    }
});

async function getLastMatchFlu(message) {
    try {
        const lastMatch = await webScrapingService.getLastMatch();
        client.sendMessage(message.from, lastMatch);
    } catch (error) {
        client.sendMessage(message.from, 'Ocorreu um erro ao obter a última partida.');
    }
}

client.initialize();
