const puppeteer = require ('puppeteer');


async function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}


//Cookie et Local Storage Setup
(async () => {
	const browser = await puppeteer.launch({
		userDataDir: ".user_data",
		headless:true,
		args:['--no-sandbox']
	});
	
	const HomePage = await browser.newPage();
	await HomePage.goto('http://userservice:5556/');
	
	await HomePage.type('#loginName','Gandalf');
	await HomePage.type('#loginPassword','strongpassword7890ABCDEFGHJKLMNOPQRSTUV');
	
	await HomePage.click('#submitLoginButton');
	
	await delay(2500);

    while(true) {
    	await HomePage.click('#getFaqButton');
		await delay(2500);
	}
})();
	