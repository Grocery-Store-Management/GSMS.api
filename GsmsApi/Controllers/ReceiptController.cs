﻿using BusinessObjectLibrary;
using DataAccessLibrary.BusinessEntity;
using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GsmsApi.Controllers
{
    //PhucVVT
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private ReceiptBusinessEntity receiptEntity;
        public ReceiptController(IUnitOfWork work)
        {
            receiptEntity = new ReceiptBusinessEntity(work);
        }

        // GET: api/<ReceiptController>
        /// <summary>
        /// Get All the receipts
        /// </summary>
        /// <returns>List of receipts</returns>
        [HttpGet]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<Receipt>), 200)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                IEnumerable<Receipt> receipts = await receiptEntity.GetReceiptsAsync();
                return StatusCode(200, receipts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<ReceiptController>/5
        /// <summary>
        /// Get receipt based on ID
        /// </summary>
        /// <param name="id">The ID string of the receipt</param>
        /// <returns>The existed Receipt</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Receipt), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                Receipt receipt = await receiptEntity.GetAsync(id);
                if (receipt == null)
                {
                    return StatusCode(204);
                }
                return StatusCode(200, receipt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // POST api/<ReceiptController>
        /// <summary>
        /// Add a new receipt
        /// </summary>
        /// <param name="newReceipt">New Receipt to be added</param>
        /// <returns>The added import order</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Receipt), 201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostAsync([FromBody] Receipt newReceipt)
        {
            try
            {
                Receipt addedReceipt = await receiptEntity.AddReceiptAsync(newReceipt);
                return StatusCode(201, addedReceipt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //PUT api/<ReceiptController>/5
        /// <summary>
        /// Update receipt
        /// </summary>
        /// <param name="id">ID of the receipt to be updated</param>
        /// <param name="updatedReceipt">New information to update the receipt</param>
        /// <returns>The updated receipt</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Receipt), 201)]
        public async Task<IActionResult> Put(string id, [FromBody] Receipt updatedReceipt)
        {
            try
            {
                if (!id.Equals(updatedReceipt.Id))
                {
                    throw new Exception("The ID is not the same!!");
                }
                Receipt receipt = await receiptEntity.UpdateReceiptAsync(updatedReceipt);
                return StatusCode(200, receipt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ReceiptController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await receiptEntity.DeleteReceiptAsync(id);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}