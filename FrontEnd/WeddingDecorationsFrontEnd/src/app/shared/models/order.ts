export interface Order {
  id?: number;
  customer: string;
  dateWhenOrderWasMade: string;
  dateForOrderToBeCompleted: string;
  location: string;
  type: string;
}
