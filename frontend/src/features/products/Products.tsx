import { useQuery } from "@tanstack/react-query";
import { useSearchParams } from "react-router-dom";

import { fetchProducts } from "../../services/apiProducts";
import Spinner from "../../ui/Spinner";
import { ProductCard } from "./ProductCard";

export const Products = () => {
  const [searchParams] = useSearchParams();
  const discountType = searchParams.get("discount") || "all";
  const filteredBy = searchParams.get("filteredBy") || "all";
  const orderBy = searchParams.get("orderBy") || "name-asc";
  const page = searchParams.get("page") || 1;

  const {
    data: products,
    error,
    isLoading,
  } = useQuery({
    queryKey: ["product", discountType, filteredBy, orderBy, page],
    queryFn: () => fetchProducts(discountType, filteredBy, orderBy, page),
  });

  if (isLoading) return <Spinner />;

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  return (
    <>
      {products &&
        products.results.map((product: any) => (
          <ProductCard product={product} key={product.id} />
        ))}
    </>
  );
};
