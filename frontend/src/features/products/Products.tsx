import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useSearchParams } from "react-router-dom";

import { fetchProducts } from "../../services/apiProducts";
import Spinner from "../../ui/Spinner";
import { Product, ProductCard } from "./ProductCard";
import { useProductsContext } from "./ProductsProvider";

export const Products = () => {
  const productsContext = useProductsContext();
  const queryClient = useQueryClient();
  const [searchParams] = useSearchParams();
  const { onSetValues } = useProductsContext();

  const search = searchParams.get("search") || "";
  const discountType = searchParams.get("discount") || "all";
  const filteredBy = searchParams.get("filteredBy") || "all";
  const orderBy = searchParams.get("orderBy") || "name-asc";
  const page = searchParams.get("page") || 1;
  const pageSize = searchParams.get("pageSize") || "5";
  const pageCount = Math.ceil(productsContext.totalCount / +pageSize);

  const callback = (totalCount: number, pageCount: number) => {
    onSetValues(totalCount, pageCount);
  };

  const {
    data: products,
    error,
    isLoading,
  } = useQuery({
    queryKey: [
      "product",
      search,
      discountType,
      filteredBy,
      orderBy,
      page,
      pageSize,
    ],
    queryFn: () =>
      fetchProducts(
        search,
        discountType,
        filteredBy,
        orderBy,
        +page,
        pageSize,
        callback
      ),
  });

  if (+page < pageCount)
    queryClient.prefetchQuery({
      queryKey: [
        "product",
        search,
        discountType,
        filteredBy,
        orderBy,
        page,
        pageSize,
      ],
      queryFn: () =>
        fetchProducts(
          search,
          discountType,
          filteredBy,
          orderBy,
          +page + 1,
          pageSize,
          callback
        ),
    });

  if (+page > 1)
    queryClient.prefetchQuery({
      queryKey: [
        "product",
        search,
        discountType,
        filteredBy,
        orderBy,
        page,
        pageSize,
      ],
      queryFn: () =>
        fetchProducts(
          search,
          discountType,
          filteredBy,
          orderBy,
          +page - 1,
          pageSize,
          callback
        ),
    });

  if (isLoading) return <Spinner />;

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  return (
    <>
      {products &&
        products.results.map((product: Product) => (
          <ProductCard product={product} key={product.id} />
        ))}
    </>
  );
};
