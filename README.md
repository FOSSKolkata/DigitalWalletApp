# LLD for Digital Wallet


# **Mandatory Requirements**

- **You are supposed to create a digital wallet system that allows people to transfer amounts between their wallets.**
- The wallet uses its own currency known as FkRupee(F₹).
- The account balance cannot drop below F₹ 0.00.
- The smallest amount that can be transferred between wallets is 0.0001.
- The user should be presented with options for each action. And the options are as follows:
    1. **Create Wallet** – This option should create a wallet for the user.
    2. **Transfer Amount** – This option should enable the transfer of funds from one account to the other.
    3. **Account Statement** – This option should display the account statement for the specified user.
    4. **Overview** – This option should display all the account numbers currently in the system. Additionally, it should show the current balances for these accounts.
    5. **Exit** – The system should exit.

# **Optional Requirements**

These are the requirements that are not mandatory, but good to have. Let’s go through the optional requirements.

1. Offer 1 – When the amount is transferred from user A to user B, F₹ 10 must be added to both the sender and receiver wallets if their balance is the same.
2. Offer 2 – Whenever Offer 2 is selected, the top 3 customers with the highest number of transactions will get F₹ 10, F₹ 5, and F₹ 2 as rewards. If there is a tie between customers, i.e., if customers have the same number of transactions, then the customer having higher account balance should be given preference. If there is still a tie, i.e., the customers have the same balance, then the customer whose account was created first should be given preference.
3. Add one more option called FixedDeposit <fd_amount>. And whenever the option is selected, an amount equal to <fd_amount> is parked for. If for the next 5 transactions, the account balance for that account remains above <fd_amount>, the user gets F₹ 10 as interest in their account. If the account balance goes below <fd_amount>, then the FD should be dissolved and the user would need to give the FixedDeposit command again to start a new FD.
4. Consider another added bonus, display the <fd_amount> and remaining transactions in the Overview and Statement command also.

# **Solution**
I have created 3 branches (other than the main branch): one for mandatory requirements, and two for optional requirements.
The main branch contains the full code. 
1. [mandatory-requirements](https://github.com/FOSSKolkata/DigitalWalletApp/tree/mandatory-requirements) - This branch contains the solution for the mandatory requirements listed above.
2. [offer](https://github.com/FOSSKolkata/DigitalWalletApp/tree/offer) - This branch contains the solution for the optional requirements 1 and 2. Note that I did some code refactoring and bug fixes in the code for offer2 later on. You will find these changes in the main branch.
3. [fixed-deposit](https://github.com/FOSSKolkata/DigitalWalletApp/tree/fixed-deposit) - This branch contains the solution for the optional requirements 3 and 4.

# **Resources**   
https://techwithkp.com/digital-wallet-design-machine-coding-round-solution/
