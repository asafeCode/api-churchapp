import { randomIntBetween, randomDate } from '../utils/random.js';

export function InflowBuilder() {
    return {
        withDate(start, end) {
            const date = new Date(
                start.getTime() + Math.random() * (end.getTime() - start.getTime())
            );

            this.date = date.toISOString().split('T')[0]; // yyyy-MM-dd
            return this;
        },

        withType() {
            this.type = Math.floor(Math.random() * 3); // 0..2
            return this;
        },

        withPaymentMethod() {
            this.paymentMethod = Math.floor(Math.random() * 2); // 0..1
            return this;
        },

        withAmount(min = 10, max = 500) {
            this.amount = Math.floor(Math.random() * (max - min + 1)) + min;
            return this;
        },

        withDescription() {
            this.description = `Entrada k6 ${Math.floor(Math.random() * 10000)}`;
            return this;
        },

        withUser(userId) {
            this.userId = userId;
            return this;
        },

        withWorship(worshipId) {
            this.worshipId = worshipId;
            return this;
        },

        build() {
            return {
                date: this.date,
                type: this.type,
                paymentMethod: this.paymentMethod,
                amount: this.amount,
                description: this.description,
                //userId: this.userId,
                worshipId: this.worshipId
            };
        }
    };
}

