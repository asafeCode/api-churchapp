export function OutflowBuilder() {
    return {
        withDate(start, end) {
            this.date = randomDate(start, end);
            return this;
        },
        withPaymentMethod() {
            this.paymentMethod = randomItem([
                'CASH',
                'PIX',
                'CREDIT_CARD',
                'DEBIT_CARD'
            ]);
            return this;
        },
        withAmount(min, max) {
            this.amount = randomIntBetween(min, max);
            return this;
        },
        withDescription() {
            this.description = `Saída ${randomIntBetween(1000, 9999)}`;
            return this;
        },
        withExpense(expenseId) {
            this.expenseId = expenseId;
            return this;
        },
        build() {
            return {
                date: this.date,
                paymentMethod: this.paymentMethod,
                amount: this.amount,
                description: this.description,
                expenseId: this.expenseId
            };
        }
    };
}
