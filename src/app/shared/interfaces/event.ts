export interface Event {
  id: string;
  title: string
  description: string;
  image: string;
  createdBy?: string;
  createdAt?: string;
  isAuthorizedFor?: string[];
}
